import type { Autor } from '../../models/Autor';

interface AutorListProps {
  autores: Autor[];
  onEditar: (autor: Autor) => void;
  onDeletar: (id: number) => void;
}

export const AutorList = ({ autores, onEditar, onDeletar }: AutorListProps) => (
  <ul className="space-y-4">
    {autores.map((autor) => (
      <li
        key={autor.id}
        className="flex justify-between items-center border border-gray-200 rounded p-3 shadow-sm hover:shadow-md transition"
      >
        <span className="text-gray-900 font-medium">{autor.nome}</span>
        <div className="flex gap-4">
          <button
            onClick={() => onEditar(autor)}
            className="text-blue-600 hover:text-blue-800 transition"
          >
            Editar
          </button>
          <button
            onClick={() => onDeletar(autor.id)}
            className="text-red-600 hover:text-red-800 transition"
          >
            Deletar
          </button>
        </div>
      </li>
    ))}
  </ul>
);

