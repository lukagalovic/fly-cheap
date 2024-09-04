import { MdArrowForward } from 'react-icons/md'
import Header from '../components/Header'
import { NavLink } from 'react-router-dom'

const HomePage = () => {
  return (
    <>
        <Header title="Welcome to Fly Cheap!" subtitle='Fly Cheap is application for searching low-cost flights. It uses Amadeus API to fetch data about flights and serve it to users.' />
        <div className="flex justify-center mt-8">
        <NavLink to="/flights">
          <MdArrowForward style={{ fontSize: '30rem'}} className="m-auto cursor-pointer hover:text-gray-300 transition duration-300" />
        </NavLink>
      </div>
    </>
  )
}

export default HomePage
