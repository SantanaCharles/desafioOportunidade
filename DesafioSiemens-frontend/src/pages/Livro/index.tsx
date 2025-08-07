import { useEffect, useState } from 'react';
import { useLivroStore } from '../../stores/livroStore';
import { useAutorStore } from '../../stores/autorStore';
import { useGeneroStore } from '../../stores/generoStore';
import { LivroForm } from '../../components/livros/LivroForm';
import { LivroList } from '../../components/livros/LivroList';
import type { Livro } from '../../models/Livro';

export const LivrosPage = () => {
  const {
    livros,
    loading,
    erro,
    carregarLivros,
    adicionarLivro,
    editarLivro,
    excluirLivro,
  } = useLivroStore();
  const { autores, carregarAutores } = useAutorStore();
  const { generos, carregarGeneros } = useGeneroStore();

  const [modalAberto, setModalAberto] = useState(false);
  const [livroSelecionado, setLivroSelecionado] = useState<Livro | null>(null);

  useEffect(() => {
    carregarLivros();
    carregarAutores();
    carregarGeneros();
  }, []);

  const abrirModalCriacao = () => {
    setLivroSelecionado(null);
    setModalAberto(true);
  };

  const abrirModalEdicao = (livro: Livro) => {
    setLivroSelecionado(livro);
    setModalAberto(true);
  };

  const fecharModal = () => {
    setLivroSelecionado(null);
    setModalAberto(false);
  };

  const salvarLivro = async (dados: { titulo: string; autor: any; genero: any }) => {
    if (livroSelecionado) {
      await editarLivro(livroSelecionado.id, {
          ...livroSelecionado,
          ...dados,
          getTituloFormatado: function (): string {
              throw new Error('Function not implemented.');
          }
      });
    } else {
      await adicionarLivro({
          titulo: dados.titulo,
          autorId: dados.autor.id,
          generoId: dados.genero.id,
          getTituloFormatado: function (): string {
              throw new Error('Function not implemented.');
          }
      });
    }
    fecharModal();
  };

  return (
    <div className="p-6 max-w-4xl mx-auto">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">Livros</h1>

      <div className="flex justify-center mb-6">
        <button
          onClick={abrirModalCriacao}
          className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700 transition"
        >
          Adicionar Livro
        </button>
      </div>

      {loading && <p className="text-gray-500 text-center">Carregando livros...</p>}
      {erro && <p className="text-red-600 text-center">{erro}</p>}

      <LivroList livros={livros} onEditar={abrirModalEdicao} onDeletar={excluirLivro} />

      {modalAberto && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg shadow-lg p-6 w-full max-w-sm">
            <LivroForm
              livro={livroSelecionado}
              autores={autores}
              generos={generos}
              onSalvar={salvarLivro}
              onCancelar={fecharModal}
            />
          </div>
        </div>
      )}
    </div>
  );
};
