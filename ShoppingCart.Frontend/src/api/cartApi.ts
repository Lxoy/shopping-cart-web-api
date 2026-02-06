import { API_URL } from "./config";
import type { Cart } from "../types/Cart";

export const getCart = async (userId: number): Promise<Cart> => {
    const res = await fetch(`${API_URL}/cart/${userId}`);

    if (!res.ok) {
        throw new Error("Failed to fetch cart");
    }

    return res.json();
};

export const clearCart = async (userId: number): Promise<void> => {
    const res = await fetch(`${API_URL}/cart/${userId}/items`, {
        method: "DELETE",
    });

    if (!res.ok) {
        throw new Error("Failed to delete items from cart");
    }
};

export const addToCart = async (userId: number, articleId: number,): Promise<void> => {
    const res = await fetch(`${API_URL}/cart/${userId}/items/${articleId}`, {
        method: "POST"
    });

     if (!res.ok) throw new Error("Failed to add item to the cart");
}

export const removeItem = async (userId: number, cartItemId: number): Promise<void> => {
    const res = await fetch(`${API_URL}/cart/${userId}/items/${cartItemId}`, {
        method: "DELETE"
    });

     if (!res.ok) throw new Error("Failed to delete item from the cart");
}