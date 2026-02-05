import { useEffect, useState } from "react"
import { clearCart, getCart, removeItem } from "../api/cartApi";
import type { Cart } from "../types/Cart";
import './CartPage.css';
import CartItemList from "../components/CartItemsList";

export default function CartPage() {
    const [error, setError] = useState<string | null>(null);
    const [cart, setCart] = useState<Cart>();
    const [loading, setLoading] = useState(true);
    const userId = 1;

    const loadCart = async () => {
        try {
            const data = await getCart(userId);
            setCart(data);
        } catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            } else {
                setError("Unknown error occurred");
            }
        }
        finally {
            setLoading(false);
        }
    }

    const handleClearCart = async () => {
        try {
            await clearCart(userId);
        } catch (err) {
            setError(err instanceof Error ? err.message : "Unknown error occurred");
        } finally {
            loadCart();
        }
    };

    const handleItemDelete = async (articleId: number) => {
        try {
            await removeItem(userId, articleId);
        } catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            } else {
                setError("Unknown error occurred");
            }
        }
        finally {
            loadCart();
        }
    }

    useEffect(() => {
        loadCart()

    }, []);

    if (loading) {
        return (
            <div className="error-box">
                <p className="message">Loading cart...</p>
            </div>
        );
    }


    if (error) {
        return (
            <div className="error-box">
                <p className="message">{error}</p>
            </div>
        );
    }

    if (cart!) {
        return (
            <div className="cart-page">
                <a href="/articles" className="article-nav">Articles</a>

                <div className="cart-container">
                    <h2 className="cart-title">Your Cart</h2>

                    <div className="cart-list">
                        <CartItemList
                            cartItems={cart.items}
                            onDelete={handleItemDelete}
                        />
                    </div>

                    <div className="cart-footer">
                        <button
                            className="remove-cart-button"
                            onClick={handleClearCart}
                        >
                            Delete
                        </button>

                        <h3 className="cart-total">Price: €{cart.totalPrice}</h3>
                    </div>
                </div>
            </div>
        );
    }

}