const Footer = () => {
  return (
    <footer className="bg-indigo-700 text-white py-4 mt-auto">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
        <p className="text-sm">
          Created by <span className="font-bold">Luka Galovic</span> &copy; {new Date().getFullYear()}
        </p>
      </div>
    </footer>
  );
};

export default Footer;
