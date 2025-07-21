import React from "react";
import {
  Box,
  Typography,
  Avatar,
  Button,
  Divider,
  TextField,
  Grid,
  Card,
  CardContent,
  Paper,
  InputAdornment,
  IconButton,
} from "@mui/material";
import {
  Edit as EditIcon,
  Email as EmailIcon,
  Phone as PhoneIcon,
  LocationOn as LocationIcon,
  Public as PublicIcon,
  Person as PersonIcon,
  Work as WorkIcon,
  School as SchoolIcon,
  CalendarToday as CalendarIcon,
  PersonPin
} from "@mui/icons-material";

import AvatarMale from "@/assets/defaultAvatarMale.png";
import AvatarFemale from "@/assets/defaultAvatarFemale.png";
import UserProfile from "../components/UserProfile";

const ProfilePage: React.FC = () => {
  const [tab, setTab] = React.useState(0);

  const handleTabChange = (_: React.SyntheticEvent, newValue: number) => {
    setTab(newValue);
  };

  return (
    <Box sx={{ p: 3 }}>
      {/* Заголовок страницы */}
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          mb: 4,
        }}
      >
        <Typography variant="h5" component="h1" fontWeight="bold">
          Мой профиль
        </Typography>
        {/* <Button variant="contained" startIcon={<EditIcon />}>
          Редактировать
        </Button> */}
      </Box>

      {/* Блок с аватаром на фоновом изображении */}
      <UserProfile />

      {/* Форма редактирования - полная ширина */}
      {/* <Card sx={{ boxShadow: 3 }}>
        <CardContent>
          <Typography
            variant="h6"
            fontWeight="bold"
            gutterBottom
            sx={{ mb: 3 }}
          >
            Редактировать профиль
          </Typography>

          

          <Divider sx={{ my: 4 }} />

          <Typography
            variant="h6"
            fontWeight="bold"
            gutterBottom
            sx={{ mb: 3 }}
          >
            О себе
          </Typography>

          <TextField
            fullWidth
            multiline
            rows={4}
            defaultValue="Специализируюсь на дизайне пользовательского интерфейса и взаимодействия, дизайне продуктов, визуальном дизайне и многом другом. У меня есть страсть к улучшению жизни других людей через дизайн и постоянно ищу новые вещи, которые можно узнать и создать."
          />

          <Box sx={{ display: "flex", justifyContent: "flex-end", mt: 4 }}>
            <Button variant="outlined" sx={{ mr: 2 }}>
              Отменить
            </Button>
            <Button variant="contained">Сохранить изменения</Button>
          </Box>
        </CardContent>
      </Card> */}
    </Box>
  );
};

export default ProfilePage;
