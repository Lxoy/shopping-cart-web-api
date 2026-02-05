import { Routes, Route, Navigate } from "react-router-dom";
import ArticlesPage from './pages/ArticlesPage';
import CartPage from "./pages/CartPage";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/articles" />} />
      <Route path="/articles" element={<ArticlesPage />} />
      <Route path="/cart" element={<CartPage />} />
    </Routes>
  )
}

export default App
