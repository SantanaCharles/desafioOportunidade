import type { Livro } from '../../models/Livro';

interface LivroListProps {
  livros: Livro[];
  onEditar: (livro: Livro) => void;
  onDeletar: (id: number) => void;
}

export const LivroList = ({ livros, onEditar, onDeletar }: LivroListProps) => (
  <ul className="space-y-4">
    {livros.map((livro) => (
      <li
        key={livro.id}
        className="flex justify-between items-center border border-gray-200 rounded p-3 shadow-sm hover:shadow-md transition"
      >
        <div>
          <p className="text-gray-900 font-semibold">{livro.titulo}</p>
          <p className="text-sm text-gray-600">
            Autor: {livro.autor} | Gênero: {livro.genero}
          </p>
        </div>
        <div className="flex gap-4">
          <button
            onClick={() => onEditar(livro)}
            className="text-blue-600 hover:text-blue-800 transition"
          >
            Editar
          </button>
          <button
            onClick={() => onDeletar(livro.id)}
            className="text-red-600 hover:text-red-800 transition"
          >
            Deletar
          </button>
        </div>
      </li>
    ))}
  </ul>
);
