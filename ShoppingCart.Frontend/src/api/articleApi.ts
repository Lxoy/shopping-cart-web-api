import { API_URL } from "./config";
import type { Article } from "../types/Article";
import type { ApiError } from "../types/ApiError";

export const getArticles = async (): Promise<Article[]> => {
    const res = await fetch(`${API_URL}/article`);
    if (!res.ok) {
        const errorData: ApiError = await res.json();
        throw errorData;
    }
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

    if (!res.ok) {
        const errorData: ApiError = await res.json();
        throw errorData;
    }
};

export const deleteArticle = async (id: number) => {
    const res = await fetch(`${API_URL}/article/${id}`, {
        method: "DELETE"
    });

    if (!res.ok) {
        const errorData: ApiError = await res.json();
        throw errorData;
    }
};