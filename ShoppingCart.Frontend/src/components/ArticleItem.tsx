import type { Article } from "../types/Article";

type Props = {
  article: Article;
  onAdd: (id: number) => void;
  onDelete: (id: number) => void;
};

export default function ArticleItem({ article, onAdd, onDelete }: Props) {
  return (
    <div className="article-item">
      <div className="article-info">
        <span className="article-name">{article.name}</span>
        <span className="article-price">€{article.price}</span>
      </div>

      <div>
        <button
        className="add-button"
        onClick={() => onAdd(article.id)}>
            Add
        </button>

      <button
        className="delete-button"
        onClick={() => onDelete(article.id)}
      >
        Delete
      </button>
      </div>
    </div>
  );
}