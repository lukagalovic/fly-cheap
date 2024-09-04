import { ChangeEvent, FormEvent, useState } from 'react';
import FlightSearchFormState from './FlightSearchFormState';

const FlightSearchForm = () => {
  const [formState, setFormState] = useState<FlightSearchFormState>({
    originLocationCode: '',
    destinationLocationCode: '',
    departureDate: '',
    returnDate: '',
    adults: 1,
    children: undefined,
    infants: undefined,
    currencyCode: 'EUR',
    maxPrice: undefined,
  });

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLSelectElement>,
  ) => {
    const { name, value } = e.target;
    setFormState((prevState) => ({
      ...prevState,
      [name]:
        name === 'adults' || name === 'maxPrice'
          ? parseInt(value) || undefined
          : value,
    }));
  };

  // Submission
  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();

    const {
      originLocationCode,
      destinationLocationCode,
      departureDate,
      returnDate,
      adults,
      children,
      infants,
      currencyCode,
      maxPrice,
    } = formState;

    const queryParams = new URLSearchParams({
      OriginLocationCode: originLocationCode,
      DestinationLocationCode: destinationLocationCode,
      DepartureDate: departureDate,
      Adults: adults.toString(),
      CurrencyCode: currencyCode,
      ...(returnDate && { ReturnDate: returnDate }),
      ...(children !== undefined && { Children: children.toString() }),
      ...(infants !== undefined && { Infants: infants.toString() }),
      ...(maxPrice !== undefined && { MaxPrice: maxPrice.toString() }),
    });

    try {
      const response = await fetch(
        `http://localhost:8080/flights?${queryParams.toString()}`,
        {
          method: 'GET',
          headers: {
            Authorization: 'Bearer YOUR_TOKEN_HERE',
            'Content-Type': 'application/json',
          },
        },
      );

      if (response.ok) {
        const data = await response.json();
        console.log('API Response:', data);
        // Handle successful response here
      } else {
        const errorData = await response.text();
        console.error('Error:', errorData);
        // Handle error response here
      }
    } catch (error) {
      console.error('Exception:', error);
      // Handle network error here
    }
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="bg-indigo-500 p-6 rounded-md shadow-md max-w-4xl mx-auto"
    >
      <h2 className="text-white text-2xl font-bold mb-4">Search Flights</h2>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
        <div className="flex flex-col">
          <label
            htmlFor="originLocationCode"
            className="block text-white font-medium mb-1"
          >
            From:
          </label>
          <input
            type="text"
            id="originLocationCode"
            name="originLocationCode"
            value={formState.originLocationCode}
            onChange={handleChange}
            placeholder="Enter origin"
            required
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="flex flex-col">
          <label
            htmlFor="destinationLocationCode"
            className="block text-white font-medium mb-1"
          >
            To:
          </label>
          <input
            type="text"
            id="destinationLocationCode"
            name="destinationLocationCode"
            value={formState.destinationLocationCode}
            onChange={handleChange}
            placeholder="Enter destination"
            required
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
        <div className="flex flex-col">
          <label
            htmlFor="departureDate"
            className="block text-white font-medium mb-1"
          >
            Departure Date:
          </label>
          <input
            type="date"
            id="departureDate"
            name="departureDate"
            value={formState.departureDate}
            onChange={handleChange}
            required
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="flex flex-col">
          <label
            htmlFor="returnDate"
            className="block text-white font-medium mb-1"
          >
            Return Date:
          </label>
          <input
            type="date"
            id="returnDate"
            name="returnDate"
            value={formState.returnDate || ''}
            onChange={handleChange}
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
        <div className="flex flex-col">
          <label htmlFor="adults" className="block text-white font-medium mb-1">
            Adults:
          </label>
          <input
            type="number"
            id="adults"
            name="adults"
            value={formState.adults}
            onChange={handleChange}
            min="1"
            max="9"
            required
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="flex flex-col">
          <label
            htmlFor="currencyCode"
            className="block text-white font-medium mb-1"
          >
            Currency Code:
          </label>
          <select
            id="currencyCode"
            name="currencyCode"
            value={formState.currencyCode}
            onChange={handleChange}
            required
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="USD">USD</option>
            <option value="EUR">EUR</option>
            <option value="CHF">CHF</option>
          </select>
        </div>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
        <div className="flex flex-col">
          <label
            htmlFor="maxPrice"
            className="block text-white font-medium mb-1"
          >
            Max Price:
          </label>
          <input
            type="number"
            id="maxPrice"
            name="maxPrice"
            value={formState.maxPrice || ''}
            onChange={handleChange}
            min="0"
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
      </div>
      <div className="flex justify-end">
        <button
          type="submit"
          className="bg-black text-white px-6 py-3 rounded-md hover:bg-gray-900 focus:outline-none focus:ring-2 focus:ring-indigo-500"
        >
          Search Flights
        </button>
      </div>
    </form>
  );
};

export default FlightSearchForm;
