import { useEffect, useState } from "react";
import type { Article } from "../types/Article";
import { getArticles, deleteArticle } from "../api/articleApi";
import "./ArticlesPage.css";
import ArticleForm from "../components/ArticleForm";
import ArticleList from "../components/ArticleList";
import { addToCart } from "../api/cartApi";
import AddMenu from "../components/AddMenu";

export default function ArticlesPage() {
  const [articles, setArticles] = useState<Article[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedArticleId, setSelectedArticleId] = useState<number | null>(null);
  const [quantity, setQuantity] = useState(1);
  const userId = 1;

  const loadArticles = async () => {
    try {
      const data = await getArticles();
      setArticles(data);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadArticles();
  }, []);

  const handleDelete = async (id: number) => {
    await deleteArticle(id);
    setArticles(prev => prev.filter(a => a.id !== id));
  };

  const openAddMenu = (id: number) => {
    setSelectedArticleId(id);
    setQuantity(1);
  };

  const handleConfirmAdd = async (articleId: number, quantity: number) => {
    await addToCart(userId, {
      articleId,
      quantity
    });
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div className="article-page">
      <a href="/cart" className="cart-nav">Cart</a>
      {selectedArticleId &&
        <div className="add-menu">
          <AddMenu
            selectedArticleId={selectedArticleId}
            quantity={quantity}
            setQuantity={setQuantity}
            onConfirm={handleConfirmAdd}
            onClose={() => {
              setSelectedArticleId(null);
              setQuantity(1);
            }}
          />
        </div>}
      <div className="article-form">
        <div>
          <h2 style={{ textAlign: "center", marginBottom: "16px", color: "#fff" }}>
            Add article
          </h2>
          <ArticleForm onCreated={loadArticles} />
        </div>
      </div>


      <div className="article-list-wrapper">
        <ArticleList
          articles={articles}
          onAdd={openAddMenu}
          onDelete={handleDelete}
        />
      </div>
    </div>
  );
}
