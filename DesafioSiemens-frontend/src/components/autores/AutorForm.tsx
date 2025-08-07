import { useState, useEffect } from 'react';
import type { Autor } from '../../models/Autor';

interface AutorFormProps {
  autor?: Autor | null;
  onSalvar: (nome: string) => void;
  onCancelar: () => void;
}

export const AutorForm = ({ autor, onSalvar, onCancelar }: AutorFormProps) => {
  const [nome, setNome] = useState('');

  useEffect(() => {
    setNome(autor?.nome || '');
  }, [autor]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!nome.trim()) return alert('Nome do autor é obrigatório.');
    onSalvar(nome.trim());
  };

  return (
    <form onSubmit={handleSubmit} className="p-4">
      <h2 className="text-xl font-semibold mb-4">
        {autor ? 'Editar Autor' : 'Adicionar Autor'}
      </h2>

      <input
        type="text"
        placeholder="Nome do autor"
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
