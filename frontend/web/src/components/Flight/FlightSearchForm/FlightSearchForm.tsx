import { ChangeEvent, FormEvent, useState } from 'react';
import FlightSearchFormState from './FlightSearchFormState';

interface FlightSearchFormProps {
  onSubmit: (formState: FlightSearchFormState) => void;
}

const FlightSearchForm = ({ onSubmit }: FlightSearchFormProps) => {
  const [formState, setFormState] = useState<FlightSearchFormState>({
    originLocationCode: '',
    destinationLocationCode: '',
    departureDate: '',
    returnDate: '',
    adults: 1,
    currencyCode: 'EUR',
    maxPrice: undefined,
  });

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLSelectElement>,
  ) => {
    const { name, value } = e.target;

    const transformedValue =
      name === 'originLocationCode' || name === 'destinationLocationCode'
        ? value.trim().toUpperCase()
        : value;

    setFormState((prevState) => ({
      ...prevState,
      [name]:
        name === 'adults' || name === 'maxPrice'
          ? parseInt(transformedValue) || undefined
          : transformedValue,
    }));
  };

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    onSubmit(formState);
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="bg-indigo-500 p-6 rounded-md shadow-md max-w-4xl mx-auto flex flex-col gap-4"
    >
      <h2 className="text-white text-2xl font-bold mb-4">Search Flights</h2>

      <div className="flex flex-wrap gap-4 mb-4">
        <div className="flex-1 min-w-[150px]">
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
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="flex-1 min-w-[150px]">
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
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="flex-1 min-w-[150px]">
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
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="flex-1 min-w-[150px]">
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
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="flex-1 min-w-[150px]">
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
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="flex-1 min-w-[150px]">
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
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="USD">USD</option>
            <option value="EUR">EUR</option>
            <option value="CHF">CHF</option>
          </select>
        </div>
        <div className="flex-1 min-w-[150px]">
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
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
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
