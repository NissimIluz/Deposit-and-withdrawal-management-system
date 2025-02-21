import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { useDispatch, useSelector } from "react-redux";
import { processTransaction, setUserIdAndLoadHistory } from "../store/transactionsSlice";
import { RootState, AppDispatch } from "../store/store";
import { TextField, MenuItem, Button, Box, Typography, List, ListItem } from "@mui/material";

const schema = yup.object({
  fullNameHebrew: yup.string().matches(/^[א-ת\s'’-]+$/, "עברית בלבד").max(20).required(),
  fullNameEnglish: yup.string().matches(/^[A-Za-z\s'’-]+$/, "אנגלית בלבד").max(15).required(),
  birthDate: yup.date().required(),
  userId: yup.string().matches(/^\d{9}$/, "תעודת זהות לא תקינה").required(),
  transactionType: yup.string().oneOf(["deposit", "withdrawal"]).required(),
  amount: yup.number().positive().max(9999999999).required(),
  bankAccount: yup.string().matches(/^\d{10}$/, "מספר חשבון לא תקין").required(),
});

export default function TransactionForm() {
  const dispatch = useDispatch<AppDispatch>();
  const { register, handleSubmit, formState: { errors } } = useForm({ resolver: yupResolver(schema) });

  const [userId, setUserIdState] = useState(""); // Local state for userId

  // Effect: Dispatch setUserId when userId changes
  useEffect(() => {
    dispatch(setUserIdAndLoadHistory(userId));
  }, [userId, dispatch]);

  const handleUserIdChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUserIdState(event.target.value); // Update local state
  };

  const onSubmit = async (data: any) => {
    await dispatch(processTransaction(data));
  };

  return (
    <Box
      dir="rtl"
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{ p: 2, bgcolor: "white", borderRadius: 2 }}
      className="transaction-form"
    >
      <TextField
        {...register("userId")}
        label="תעודת זהות"
        fullWidth
        error={!!errors.userId}
        helperText={errors.userId?.message}
        value={userId}
        onChange={handleUserIdChange} // Update state on input change
      />
      <TextField {...register("fullNameHebrew")} label="שם מלא בעברית" fullWidth error={!!errors.fullNameHebrew} helperText={errors.fullNameHebrew?.message}  />
      <TextField {...register("fullNameEnglish")} label="שם מלא באנגלית" fullWidth error={!!errors.fullNameEnglish} helperText={errors.fullNameEnglish?.message}  />
      <TextField type="date" {...register("birthDate")} fullWidth error={!!errors.birthDate} helperText={errors.birthDate?.message}  />
      <TextField select {...register("transactionType")} label="פעולה" fullWidth >
        <MenuItem value="deposit">הפקדה</MenuItem>
        <MenuItem value="withdrawal">משיכה</MenuItem>
      </TextField>
      <TextField {...register("amount")} label="סכום" fullWidth error={!!errors.amount} helperText={errors.amount?.message}  />
      <TextField {...register("bankAccount")} label="מספר חשבון" fullWidth error={!!errors.bankAccount} helperText={errors.bankAccount?.message}  />
      
      <Button type="submit" variant="contained" color="primary" >
        בצע פעולה
      </Button>

     
    </Box>
  );
}
