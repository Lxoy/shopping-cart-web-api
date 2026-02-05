import type { Article } from "../types/Article";

const API_URL = import.meta.env.VITE_API_URL;

export const getArticles = async (): Promise<Article[]> => {
    const res = await fetch(`${API_URL}/article`);
    if (!res.ok) throw new Error("Failed to fetch articles");
    return res.json();
}

export const createArticle = async (data: {
    name: string;
    price: number;
}) => {
    const res = await fetch(`${API_URL}/article`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });

    if (!res.ok) throw new Error("Failed to create article");
};

export const deleteArticle = async (id: number) => {
    const res = await fetch(`${API_URL}/article/${id}`, {
        method: "DELETE"
    });

    if (!res.ok) throw new Error("Failed to delete article");
};