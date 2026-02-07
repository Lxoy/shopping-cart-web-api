import type { Article } from "../../types/Article";
import ArticleItem from "./ArticleItem";
import './ArticleList.css'

type Props = {
  articles: Article[];
  onAdd: (id: number) => void;
  onDelete: (id: number) => void;
};

export default function ArticleList({ articles, onAdd, onDelete }: Props) {
  return (
    <div className="article-list-card">
      <h2 className="article-list-title">List of all articles</h2>

      <div className="article-list-scroll">
        {articles.map(article => (
          <ArticleItem
            key={article.id}
            article={article}
            onAdd={onAdd}
            onDelete={onDelete}
          />
        ))}
      </div>
    </div>
  );
}