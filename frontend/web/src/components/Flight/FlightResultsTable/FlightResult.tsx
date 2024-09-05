export default interface FlightResult {
  id: string;
  origin: string;
  destination: string;
  departureDate: string;
  returnDate?: string;
  outboundTransfers: number;
  returnTransfers?: number;
  numberOfPassengers: number;
  currencyCode: string;
  totalPrice: number;
}
