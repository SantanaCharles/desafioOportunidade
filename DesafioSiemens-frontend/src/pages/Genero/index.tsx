import { useEffect, useState } from 'react';
import { useGeneroStore } from '../../stores/generoStore';
import { GeneroList } from '../../components/genero/GeneroList';
import { GeneroForm } from '../../components/genero/GeneroForm';
import type { Genero } from '../../models/Genero';

export const GenerosPage = () => {
  const {
    generos,
    loading,
    erro,
    carregarGeneros,
    adicionarGenero,
    deletarGenero,
    editarGenero,
  } = useGeneroStore();

  const [modalAberto, setModalAberto] = useState(false);
  const [generoSelecionado, setGeneroSelecionado] = useState<Genero | null>(null);

  useEffect(() => {
    carregarGeneros();
  }, []);

  const abrirModalCriacao = () => {
    setGeneroSelecionado(null);
    setModalAberto(true);
  };

  const abrirModalEdicao = (genero: Genero) => {
    setGeneroSelecionado(genero);
    setModalAberto(true);
  };

  const fecharModal = () => {
    setGeneroSelecionado(null);
    setModalAberto(false);
  };

  const salvarGenero = async (nome: string) => {
    if (generoSelecionado) {
      await editarGenero(generoSelecionado.id, {
        ...generoSelecionado,
        nome,
        getQuantidadeLivros: () => generoSelecionado.livros?.length || 0,
      });
    } else {
      await adicionarGenero({
        nome,
        livros: [],
        getQuantidadeLivros: () => 0,
      });
    }
    fecharModal();
  };

  return (
    <div className="p-6 max-w-4xl mx-auto">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">Gêneros</h1>

      <div className="flex justify-center mb-6">
        <button
          onClick={abrirModalCriacao}
          className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700 transition"
        >
          Adicionar Gênero
        </button>
      </div>

      {loading && <p className="text-gray-500 text-center">Carregando gêneros...</p>}
      {erro && <p className="text-red-600 text-center">{erro}</p>}

      <GeneroList generos={generos} onEditar={abrirModalEdicao} onDeletar={deletarGenero} />

      {modalAberto && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg shadow-lg p-6 w-full max-w-sm">
            <GeneroForm
              genero={generoSelecionado}
              onSalvar={salvarGenero}
              onCancelar={fecharModal}
            />
          </div>
        </div>
      )}
    </div>
  );
};
