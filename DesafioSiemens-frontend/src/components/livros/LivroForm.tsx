import { useEffect, useState } from 'react';
import type { Livro } from '../../models/Livro';
import type { Autor } from '../../models/Autor';
import type { Genero } from '../../models/Genero';

interface LivroFormProps {
  livro?: Livro | null;
  autores: Autor[];
  generos: Genero[];
  onSalvar: (dados: { titulo: string; autorId: number; generoId: number }) => void;
  onCancelar: () => void;
}

export const LivroForm = ({ livro, autores, generos, onSalvar, onCancelar }: LivroFormProps) => {
  const [titulo, setTitulo] = useState(livro?.titulo || '');
  const [autorSelecionado, setAutorSelecionado] = useState<Autor | null>(
  livro?.autorId ? autores.find(a => a.id === livro.autorId) ?? null : null
);
const [generoSelecionado, setGeneroSelecionado] = useState<Genero | null>(
  livro?.generoId ? generos.find(g => g.id === livro.generoId) ?? null : null
);

  useEffect(() => {
    setTitulo(livro?.titulo || '');
    setAutorSelecionado(
      livro?.autorId
      ? autores.find(a => a.id === livro.autorId) ?? null
      : null
    );
    setGeneroSelecionado(
     livro?.generoId
      ? generos.find(g => g.id === livro.generoId) ?? null
      : null
    );
  }, [livro, autores]);

  const handleSalvar = () => {
    if (!titulo.trim() || !autorSelecionado || !generoSelecionado) {
      alert('Todos os campos devem ser preenchidos.');
      return;
    }

    onSalvar({
      titulo: titulo.trim(),
      autorId: autorSelecionado?.id ?? null,
      generoId: generoSelecionado?.id ?? null,
    });
  };

  return (
    <div>
      <h2 className="text-xl font-semibold mb-4 text-gray-800">
        {livro ? 'Editar Livro' : 'Adicionar Livro'}
      </h2>

      <input
        type="text"
        placeholder="Título do livro"
        value={titulo}
        onChange={(e) => setTitulo(e.target.value)}
        className="w-full border border-gray-300 rounded px-3 py-2 mb-4"
      />

      <select
        value={autorSelecionado?.id || ''}
        onChange={(e) =>
          setAutorSelecionado(autores.find((a) => a.id === Number(e.target.value)) || null)
        }
        className="w-full border border-gray-300 rounded px-3 py-2 mb-4"
      >
        <option value="">Selecione um autor</option>
        {autores.map((autor) => (
          <option key={autor.id} value={autor.id}>
            {autor.nome}
          </option>
        ))}
      </select>

      <select
        value={generoSelecionado?.id || ''}
        onChange={(e) =>
          setGeneroSelecionado(generos.find((g) => g.id === Number(e.target.value)) || null)
        }
        className="w-full border border-gray-300 rounded px-3 py-2 mb-4"
      >
        <option value="">Selecione um gênero</option>
        {generos.map((genero) => (
          <option key={genero.id} value={genero.id}>
            {genero.nome}
          </option>
        ))}
      </select>

      <div className="flex justify-end space-x-3">
        <button onClick={onCancelar} className="px-4 py-2 bg-gray-200 rounded hover:bg-gray-300">
          Cancelar
        </button>
        <button
          onClick={handleSalvar}
          className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
        >
          Salvar
        </button>
      </div>
    </div>
  );
};
