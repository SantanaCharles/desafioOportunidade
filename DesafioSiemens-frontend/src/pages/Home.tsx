export const Home = () => {
  return (
    <div className="text-center mt-10">
      <h1 className="text-4xl font-bold mb-4">Sistema de Biblioteca</h1>
      <p className="text-lg">Acesse os módulos:</p>
      <div className="flex justify-center gap-6 mt-8">
        <a href="/autores" className="bg-blue-600 text-white px-6 py-3 rounded-lg hover:bg-blue-800">Autores</a>
        <a href="/generos" className="bg-green-600 text-white px-6 py-3 rounded-lg hover:bg-green-800">Gêneros</a>
        <a href="/livros" className="bg-purple-600 text-white px-6 py-3 rounded-lg hover:bg-purple-800">Livros</a>
      </div>
    </div>
  );
};