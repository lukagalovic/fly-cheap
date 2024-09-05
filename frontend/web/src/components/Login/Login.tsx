import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { saveToken } from '../../shared/auth';

const Login = () => {
  const [clientId, setClientId] = useState('');
  const [clientSecret, setClientSecret] = useState('');
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();

    const apiUrl: string = import.meta.env.VITE_API_URL;

    try {
      const response = await fetch(`${apiUrl}/api/auth`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ clientId, clientSecret }),
      });

      if (!response.ok) {
        throw new Error('Invalid credentials');
      }

      const data = await response.json();
      saveToken(data.access_token);
      navigate('/flights');
    } catch (err: any) {
      setError(err.message);
    }
  };

  return (
    <form
      onSubmit={handleLogin}
      className="w-full bg-indigo-500 p-8 rounded-lg shadow-md max-w-xl m-auto"
    >
      {error && <p className="text-red-500 mb-4 text-center">{error}</p>}
      <div className="flex flex-col mb-6">
        <label htmlFor="clientId" className="block text-white font-medium mb-2">
          Client ID
        </label>
        <input
          id="clientId"
          type="text"
          value={clientId}
          onChange={(e) => setClientId(e.target.value)}
          placeholder="Enter Client ID"
          required
          className="px-4 py-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
        />
      </div>
      <div className="flex flex-col mb-6">
        <label
          htmlFor="clientSecret"
          className="block text-white font-medium mb-2"
        >
          Client Secret
        </label>
        <input
          id="clientSecret"
          type="password"
          value={clientSecret}
          onChange={(e) => setClientSecret(e.target.value)}
          placeholder="Enter Client Secret"
          required
          className="px-4 py-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
        />
      </div>
      <div className="flex justify-end">
        <button
          type="submit"
          className="bg-black text-white px-6 py-3 rounded-md hover:bg-gray-900 focus:outline-none focus:ring-2 focus:ring-indigo-500"
        >
          Login
        </button>
      </div>
    </form>
  );
};

export default Login;
