import type { Cart } from "../types/Cart";

const API_URL = import.meta.env.VITE_API_URL;

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

export const addToCart = async (userId: number, data: { articleId: number, quantity: number }): Promise<void> => {
    const res = await fetch(`${API_URL}/cart/${userId}/items`, {
        method: "POST", headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });

     if (!res.ok) throw new Error("Failed to add item to the cart");
}