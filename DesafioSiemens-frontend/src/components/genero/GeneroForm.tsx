import { useEffect, useState } from 'react';
import type { Genero } from '../../models/Genero';

interface GeneroFormProps {
  genero?: Genero | null;
  onSalvar: (nome: string) => void;
  onCancelar: () => void;
}

export const GeneroForm = ({ genero, onSalvar, onCancelar }: GeneroFormProps) => {
  const [nome, setNome] = useState('');

  useEffect(() => {
    setNome(genero?.nome || '');
  }, [genero]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!nome.trim()) return alert('O nome do gênero é obrigatório.');
    onSalvar(nome.trim());
  };

  return (
    <form onSubmit={handleSubmit} className="p-4">
      <h2 className="text-xl font-semibold mb-4">
        {genero ? 'Editar Gênero' : 'Adicionar Gênero'}
      </h2>

      <input
        type="text"
        placeholder="Nome do gênero"
        value={nome}
        onChange={(e) => setNome(e.target.value)}
        className="w-full border px-3 py-2 rounded mb-4"
      />

      <div className="flex justify-end space-x-2">
        <button
          type="button"
          onClick={onCancelar}
          className="px-4 py-2 bg-gray-300 rounded hover:bg-gray-400"
        >
          Cancelar
        </button>
        <button
          type="submit"
          className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
        >
          Salvar
        </button>
      </div>
    </form>
  );
};
