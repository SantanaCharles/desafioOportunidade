import { useEffect, useState } from 'react';
import { useAutorStore } from '../../stores/autorStore';
import { AutorList } from '../../components/autores/AutorList';
import { AutorForm } from '../../components/autores/AutorForm';
import type { Autor } from '../../models/Autor';

export const AutoresPage = () => {
  const {
    autores,
    loading,
    erro,
    carregarAutores,
    adicionarAutor,
    editarAutor,
    deletarAutor,
  } = useAutorStore();

  const [modalAberto, setModalAberto] = useState(false);
  const [autorSelecionado, setAutorSelecionado] = useState<Autor | null>(null);

  useEffect(() => {
    carregarAutores();
  }, []);

  const abrirModalCriacao = () => {
    setAutorSelecionado(null);
    setModalAberto(true);
  };

  const abrirModalEdicao = (autor: Autor) => {
    setAutorSelecionado(autor);
    setModalAberto(true);
  };

  const fecharModal = () => {
    setModalAberto(false);
    setAutorSelecionado(null);
  };

  const salvarAutor = async (nome: string) => {
    if (autorSelecionado) {
      await editarAutor(autorSelecionado.id, {
        ...autorSelecionado,
        nome,
        getQuantidadeLivros: () => autorSelecionado.livros?.length || 0,
      });
    } else {
      await adicionarAutor({
        nome,
        livros: [],
        getQuantidadeLivros: () => 0,
      });
    }
    fecharModal();
  };

  return (
    <div className="p-6 max-w-4xl mx-auto">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">Autores</h1>

      <div className="flex justify-center mb-6">
        <button
          onClick={abrirModalCriacao}
          className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700 transition"
        >
          Adicionar Autor
        </button>
      </div>

      {loading && <p className="text-gray-500 text-center">Carregando autores...</p>}
      {erro && <p className="text-red-600 text-center">{erro}</p>}

      <AutorList autores={autores} onEditar={abrirModalEdicao} onDeletar={deletarAutor} />

      {modalAberto && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg shadow-lg p-6 w-full max-w-sm">
            <AutorForm
              autor={autorSelecionado}
              onSalvar={salvarAutor}
              onCancelar={fecharModal}
            />
          </div>
        </div>
      )}
    </div>
  );
};
