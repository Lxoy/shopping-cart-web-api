import { API_URL } from "./config";
import type { Cart } from "../types/Cart";
import type { ApiError } from "../types/ApiError";

export const getCart = async (userId: number): Promise<Cart> => {
    const res = await fetch(`${API_URL}/cart/${userId}`);

    if (!res.ok) {
        const errorData: ApiError = await res.json();
        throw errorData;
    }

    return res.json();
};

export const clearCart = async (userId: number): Promise<void> => {
    const res = await fetch(`${API_URL}/cart/${userId}/items`, {
        method: "DELETE",
    });

     if (!res.ok) {
        const errorData: ApiError = await res.json();
        throw errorData;
    }
};

export const addToCart = async (userId: number, articleId: number,): Promise<void> => {
    const res = await fetch(`${API_URL}/cart/${userId}/items/${articleId}`, {
        method: "POST"
    });

    if (!res.ok) {
        const errorData: ApiError = await res.json();
        throw errorData;
    }
}

export const changeItemQuantity = async (userId: number, cartItemId: number, quantity: number): Promise<void> => {
    const res = await fetch(`${API_URL}/cart/${userId}/items/${cartItemId}`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ quantity })
    });

     if (!res.ok) {
        const errorData: ApiError = await res.json();
        throw errorData;
    }
}

export const removeItem = async (userId: number, cartItemId: number): Promise<void> => {
    const res = await fetch(`${API_URL}/cart/${userId}/items/${cartItemId}`, {
        method: "DELETE"
    });

     if (!res.ok) {
        const errorData: ApiError = await res.json();
        throw errorData;
    }
}