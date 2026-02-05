import type { Article } from "./Article";

export interface CartItem {
    articleId: number;
    name: string;
    price: number;
    quantity: number;
    total: number;
}