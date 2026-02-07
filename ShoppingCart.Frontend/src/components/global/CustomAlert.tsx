import { useEffect } from "react";
import "./CustomAlert.css";

type Props = {
  message: string;
  type?: "error" | "success";
  onClose: () => void;
};

export default function CustomAlert({ message, type = "error", onClose }: Props) {
  useEffect(() => {
    const timer = setTimeout(() => {
      onClose();
    }, 3000);

    return () => clearTimeout(timer);
  }, [onClose]);

  return (
    <div className="alert-overlay">
      <div className={`alert-box ${type}`}>
        <span>{message}</span>
        <button onClick={onClose}>×</button>
      </div>
    </div>
  );
}
