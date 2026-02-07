import { useEffect, useState } from "react";
import type { Article } from "../types/Article";
import { getArticles, deleteArticle } from "../api/articleApi";
import "./ArticlesPage.css";
import ArticleForm from "../components/article-page/ArticleForm";
import ArticleList from "../components/article-page/ArticleList";
import { addToCart } from "../api/cartApi";
import CustomAlert from "../components/global/CustomAlert";
import { useCartContext } from "../context/CartContext";
import { Link } from "react-router-dom";
export default function ArticlesPage() {
  const { invalidItems, setInvalidItems } = useCartContext();

  const [loading, setLoading] = useState(true);
  const [success, setSuccess] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  const [articles, setArticles] = useState<Article[]>([]);
  const userId = 1;

  const loadArticles = async () => {
    try {
      const data = await getArticles();
      setArticles(data);
    } catch (err: any) {
      if (err.errorCode === "INVALID_CART_ITEMS") {
        setInvalidItems(err.invalidCartItemIds ?? []);
        setError(err.message);
      }
      else {
        setError(err.message || "Unknown error occurred");
      }
    }
    finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadArticles();
  }, []);

  const handleDelete = async (id: number) => {
    try {
      await deleteArticle(id);
      setSuccess("Article deleted successfully.");
      setArticles(prev => prev.filter(a => a.id !== id));
    } catch (err: any) {
      if (err.errorCode === "INVALID_CART_ITEMS") {
        setInvalidItems(err.invalidCartItemIds ?? []);
        setError(err.message);
      }
      else {
        setError(err.message || "Unknown error occurred");
      }
    }
  }

  const handleAdd = async (articleId: number) => {
    try {
      await addToCart(userId, articleId);
      setSuccess("Item added to cart successfully.");
    } catch (err: any) {
      if (err.errorCode === "INVALID_CART_ITEMS") {
        console.log(err);
        setInvalidItems(err.invalidCartItemIds ?? []);
        console.log(invalidItems);
        setError(err.message);
      }
      else {
        setError(err.message || "Unknown error occurred");
      }
    }
  };

  if (loading) {
    return (
      <div className="error-box">
        <p className="message">Loading articles...</p>
      </div>
    );
  }

  return (
    <div className="article-page">
      {error && (
        <CustomAlert
          message={error}
          type="error"
          onClose={() => setError(null)}
        />
      )}
      {success && (
        <CustomAlert
          message={success}
          type="success"
          onClose={() => setSuccess(null)}
        />
      )}
      <Link to="/cart" className="cart-nav">Cart</Link>
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
          onAdd={handleAdd}
          onDelete={handleDelete}
        />
      </div>
    </div>
  );
}