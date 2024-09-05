import FlightResult from './FlightResult';

const FlightResultsTable = ({ flights }: { flights: FlightResult[] }) => {
  return (
    <div className="overflow-x-auto">
      <table className="min-w-full bg-white border border-gray-200 rounded-md shadow-md">
        <thead className="bg-indigo-600 text-white">
          <tr>
            <th className="py-3 px-4 border-b border-gray-200">
              Departure Airport
            </th>
            <th className="py-3 px-4 border-b border-gray-200">
              Destination Airport
            </th>
            <th className="py-3 px-4 border-b border-gray-200">
              Departure Date
            </th>
            <th className="py-3 px-4 border-b border-gray-200">Return Date</th>
            <th className="py-3 px-4 border-b border-gray-200">
              Outbound Transfers
            </th>
            <th className="py-3 px-4 border-b border-gray-200">
              Return Transfers
            </th>
            <th className="py-3 px-4 border-b border-gray-200">
              Number of Passengers
            </th>
            <th className="py-3 px-4 border-b border-gray-200">Currency</th>
            <th className="py-3 px-4 border-b border-gray-200">Total Price</th>
          </tr>
        </thead>
        <tbody>
          {flights.map((flight) => (
            <tr key={flight.id} className="hover:bg-gray-100">
              <td className="py-3 px-4 border-b border-gray-200">
                {flight.origin}
              </td>
              <td className="py-3 px-4 border-b border-gray-200">
                {flight.destination}
              </td>
              <td className="py-3 px-4 border-b border-gray-200">
                {new Date(flight.departureDate).toLocaleString()}
              </td>
              <td className="py-3 px-4 border-b border-gray-200">
                {flight.returnDate
                  ? new Date(flight.returnDate).toLocaleString()
                  : 'N/A'}
              </td>
              <td className="py-3 px-4 border-b border-gray-200">
                {flight.outboundTransfers}
              </td>
              <td className="py-3 px-4 border-b border-gray-200">
                {flight.returnTransfers !== undefined
                  ? flight.returnTransfers
                  : 'N/A'}
              </td>
              <td className="py-3 px-4 border-b border-gray-200">
                {flight.numberOfPassengers}
              </td>
              <td className="py-3 px-4 border-b border-gray-200">
                {flight.currencyCode}
              </td>
              <td className="py-3 px-4 border-b border-gray-200">
                {flight.totalPrice.toFixed(2)}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default FlightResultsTable;
