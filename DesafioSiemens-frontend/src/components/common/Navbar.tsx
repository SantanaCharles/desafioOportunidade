import { Link } from "react-router-dom";

export const Navbar = () => {
  return (
    <nav className="bg-blue-700 text-white p-4 flex items-center justify-between">
      {/* Nome da aplicação */}
      <div className="text-xl font-bold">
        Desafio Siemens
      </div>

      {/* Links */}
      <ul className="flex gap-6 absolute left-1/2 transform -translate-x-1/2">
        <li><Link to="/" className="hover:underline">Home</Link></li>
        <li><Link to="/autores" className="hover:underline">Autores</Link></li>
        <li><Link to="/generos" className="hover:underline">Gêneros</Link></li>
        <li><Link to="/livros" className="hover:underline">Livros</Link></li>
      </ul>
    </nav>
  );
};