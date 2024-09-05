// Flights.tsx
import { useState } from 'react';
import FlightSearchFormState from './FlightSearchForm/FlightSearchFormState';
import FlightSearchForm from './FlightSearchForm/FlightSearchForm';
import FlightResultsTable from './FlightResultsTable/FlightResultsTable';
import FlightResult from './FlightResultsTable/FlightResult';
import { ClipLoader } from 'react-spinners';

function mapApiResponseToFlightResults(apiResponse: any): FlightResult[] {
  return apiResponse.data.map((flight: any) => {
    const outboundItinerary = flight.itineraries[0]; // Outbound itinerary
    const returnItinerary = flight.itineraries[1];

    return {
      id: flight.id,
      origin: outboundItinerary.segments[0].departure.iataCode,
      destination:
        outboundItinerary.segments[outboundItinerary.segments.length - 1]
          .arrival.iataCode,
      departureDate: outboundItinerary.segments[0].departure.at,
      returnDate: returnItinerary
        ? returnItinerary.segments[returnItinerary.segments.length - 1].arrival
            .at
        : undefined,
      outboundTransfers: outboundItinerary.segments.length - 1,
      returnTransfers: returnItinerary
        ? returnItinerary.segments.length - 1
        : undefined,
      numberOfPassengers: flight.travelerPricings.length,
      currencyCode: flight.price.currency,
      totalPrice: parseFloat(flight.price.total),
    };
  });
}

const Flight = () => {
  const [flightResults, setFlightResults] = useState<FlightResult[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const handleFormSubmit = async (formState: FlightSearchFormState) => {
    setLoading(true);
    setError(null);

    const queryParams = new URLSearchParams({
      originLocationCode: formState.originLocationCode,
      destinationLocationCode: formState.destinationLocationCode,
      departureDate: formState.departureDate,
      returnDate: formState.returnDate || '',
      adults: formState.adults.toString(),
      currencyCode: formState.currencyCode,
      ...(formState.maxPrice && { maxPrice: formState.maxPrice.toString() }),
    });

    try {
      const token = localStorage.getItem('access_token');
      if (!token) {
        console.error('No token found in localStorage.');
        return;
      }

      const apiUrl: string = import.meta.env.VITE_API_URL;
      const response = await fetch(
        `${apiUrl}/flights?${queryParams.toString()}`,
        {
          method: 'GET',
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
        },
      );

      if (!response.ok) {
        const errorText = await response.text();
        setError(`Fetch failed: ${errorText}`);
        console.error(`Fetch failed: ${errorText}`);
      } else {
        const result = await response.json();
        const mappedResult = mapApiResponseToFlightResults(result);
        console.log('mappedResult', mappedResult);
        setFlightResults(mappedResult);
      }
    } catch (error) {
      console.error(error);
      setError('An error occurred while fetching flights.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="flex flex-col gap-4 p-4 mx-auto">
      <FlightSearchForm onSubmit={handleFormSubmit} />
      {loading && (
        <div className="flex justify-center items-center">
          <ClipLoader color="#4F46E5" loading={loading} size={50} />
        </div>
      )}
      {error && <p className="text-red-500">{error}</p>}
      {flightResults.length > 0 && (
        <FlightResultsTable flights={flightResults} />
      )}
    </div>
  );
};

export default Flight;
