import { create } from 'zustand';
import { Livro } from '../models/Livro';
import { getLivros, createLivro, deleteLivro, updateLivro } from '../services/livroService';

interface LivroStore {
  livros: Livro[];
  loading: boolean;
  erro: string | null;
  carregarLivros: () => Promise<void>;
  excluirLivro: (id: number) => Promise<void>;
  editarLivro: (id: number, dados: Livro) => Promise<void>;
  adicionarLivro: (livro: Omit<Livro, 'id'>) => Promise<void>;
}

export const useLivroStore = create<LivroStore>((set, get) => ({
  livros: [],
  loading: false,
  erro: null,

  carregarLivros: async () => {
    set({ loading: true, erro: null });
    try {
      const response = await getLivros();
      set({ livros: response.data as Livro[], loading: false });
    } catch (err) {
      set({ erro: 'Erro ao carregar livros', loading: false });
    }
  },
  excluirLivro: async (id: number) => {
    set({ loading: true, erro: null });
    try {
      await deleteLivro(id);
      set((state) => ({
        livros: state.livros.filter((livro) => livro.id !== id),
        loading: false,
      }));
    } catch (err) {
      set({ erro: 'Erro ao excluir livro', loading: false });
    }
  },
  adicionarLivro: async (livro) => {
    set({ loading: true, erro: null });
    try {
      console.log('Adicionando livro:', livro);
      const response = await createLivro(livro);
      set((state) => ({
        livros: [...state.livros, response.data],
        loading: false,
      }));
    } catch (err) {
      set({ erro: 'Erro ao adicionar livro', loading: false });
    }
  },
   editarLivro: async (id, dados) => {
      set({ loading: true, erro: null });
      try {
        await updateLivro(id, dados);
        await get().carregarLivros();
         // Recarrega lista depois de deletar
        set({ loading: false });
     
      } catch (err) {
        set({ erro: 'Erro ao editar livro', loading: false });
      }
    }
}));
