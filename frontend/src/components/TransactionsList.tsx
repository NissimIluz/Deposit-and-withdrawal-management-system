import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { List, ListItem, Typography, CircularProgress, Box } from "@mui/material";

export default function TransactionsList() {
  const { history, loading, error } = useSelector((state: RootState) => state.transactions);

 

  return (
    <Box dir="rtl" sx={{ p: 2,  borderRadius: 2 }}>
      <Typography variant="h6" sx={{ mb: 2 }}>
        היסטוריית פעולות
      </Typography>

      {loading && <CircularProgress />}
      {error && <Typography color="error">{error}</Typography>}

      <List>
        {history.length > 0 ? (
          history.map((t, i) => (
            <ListItem key={i}> 
            {new Date(t.date).toLocaleString("en-US", {
                year: "numeric",
                month: "long",
                day: "numeric",
                hour: "2-digit",
                minute: "2-digit",
                second: "2-digit",
                hour12: false,
              })}:  
               {t.fullNameHebrew} ביצע {t.transactionType == "deposit" ? "הפקדה" : "משיכה"} בסכום של {t.amount} ₪
            </ListItem>
          ))
        ) : (
          !loading && <Typography>אין פעולות זמינות</Typography>
        )}
      </List>
    </Box>
  );
}
