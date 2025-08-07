import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home } from "../pages/Home";
import { AutoresPage } from "../pages/Autor";
import { GenerosPage } from "../pages/Genero";
import { LivrosPage } from "../pages/Livro";
import { MainLayout } from "../layouts/MainLayout";

export const AppRoutes = () => {
  return (
    <BrowserRouter>
      <MainLayout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/autores" element={<AutoresPage />} />
          <Route path="/generos" element={<GenerosPage />} />
          <Route path="/livros" element={<LivrosPage />} />
        </Routes>
      </MainLayout>
    </BrowserRouter>
  );
};
