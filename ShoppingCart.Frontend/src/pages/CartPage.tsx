import { useEffect, useState } from "react"
import { changeItemQuantity, clearCart, getCart, removeItem } from "../api/cartApi";
import type { Cart } from "../types/Cart";
import './CartPage.css';
import CartItemList from "../components/cart-page/CartItemsList";
import CustomAlert from "../components/global/CustomAlert";
import { useCartContext } from "../context/CartContext";
import { Link } from "react-router-dom";

export default function CartPage() {
    const { invalidItems, setInvalidItems } = useCartContext();

    const [loading, setLoading] = useState(true);
    const [success, setSuccess] = useState<string | null>(null);
    const [error, setError] = useState<string | null>(null);

    const [cart, setCart] = useState<Cart>();
    const userId = 1;
    console.log("invalidItems:", invalidItems);

    const loadCart = async () => {
        try {
            const data = await getCart(userId);
            setCart(data);
        }
        finally {
            setLoading(false);
        }
    }

    const handleClearCart = async () => {
        try {
            await clearCart(userId);
            setInvalidItems([]);
            setSuccess("Cart cleared successfully.");
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
            loadCart();
        }
    };

    const handleItemQuantityChange = async (itemCartId: number, quantity: number) => {
        try {
            await changeItemQuantity(userId, itemCartId, quantity);
            setInvalidItems([]);
            setSuccess("Quantity updated.");
        }
        catch (err: any) {
            if (err.errorCode === "INVALID_CART_ITEMS") {
                setInvalidItems(err.invalidCartItemIds ?? []);
                setError(err.message);
            }
            else {
                setError(err.message || "Unknown error occurred");
            }
        } finally {
            loadCart();
        }
    }

    const handleItemDelete = async (articleId: number) => {
        try {
            await removeItem(userId, articleId);
            setInvalidItems([]);
            setSuccess("Item removed from cart.");
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

    if (cart!) {
        return (
            <div className="cart-page">
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
                <Link to="/articles" className="article-nav">Articles</Link>

                <div className="cart-container">
                    <h2 className="cart-title">Your Cart</h2>

                    <div className="cart-list">
                        <CartItemList
                            cartItems={cart.items}
                            invalidItems={invalidItems}
                            onChange={handleItemQuantityChange}
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