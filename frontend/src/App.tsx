import TransactionForm from "./components/TransactionForm";
import TransactionsList from "./components/TransactionsList";

export default function App() {
  return (
    <div dir="rtl">
      <h1>ניהול עסקאות</h1>
      <TransactionForm />
      <TransactionsList />
    </div>
  );
}
