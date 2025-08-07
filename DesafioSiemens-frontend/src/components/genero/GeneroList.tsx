import type { Genero } from '../../models/Genero';

interface GeneroListProps {
  generos: Genero[];
  onEditar: (genero: Genero) => void;
  onDeletar: (id: number) => void;
}

export const GeneroList = ({ generos, onEditar, onDeletar }: GeneroListProps) => (
  <ul className="space-y-4">
    {generos.map((genero) => (
      <li
        key={genero.id}
        className="flex justify-between items-center border border-gray-200 rounded p-3 shadow-sm hover:shadow-md transition"
      >
        <span className="text-gray-900 font-medium">{genero.nome}</span>
        <div className="flex gap-4">
          <button
            onClick={() => onEditar(genero)}
            className="text-blue-600 hover:text-blue-800 transition"
          >
            Editar
          </button>
          <button
            onClick={() => onDeletar(genero.id)}
            className="text-red-600 hover:text-red-800 transition"
          >
            Deletar
          </button>
        </div>
      </li>
    ))}
  </ul>
);
