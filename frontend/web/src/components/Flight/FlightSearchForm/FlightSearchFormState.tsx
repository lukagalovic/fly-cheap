export default interface FlightSearchFormState {
  originLocationCode: string;
  destinationLocationCode: string;
  departureDate: string;
  returnDate?: string;
  adults: number;
  currencyCode: string;
  maxPrice?: number;
}
