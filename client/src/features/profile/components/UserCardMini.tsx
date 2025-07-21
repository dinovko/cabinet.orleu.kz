import type { RootState } from "@/app/store";
import { Box, Stack } from "@mui/system";
import { useSelector } from "react-redux";
import AvatarMale from "@/assets/defaultAvatarMale.png";
import AvatarFemale from "@/assets/defaultAvatarFemale.png";
import {
    Alert,
    Skeleton,
  Typography,
} from "@mui/material";

const UserCardMini = () => {
  const { user, loading, error } = useSelector(
    (state: RootState) => state.profile
  );

  if (loading) return <Skeleton variant="rounded" width={210} height={60} />;
  if (error) return <Alert variant="outlined" severity="warning" sx={{width:"100%"}}></Alert>;
  if (!user) return null;

  const avatar = user.genderId == "1" ? AvatarMale : AvatarFemale;

  return (
    <Stack direction={"row"} gap={1}>
      <Box>
        <img
          src={avatar}
          alt="Avatar"
          height={"auto"}
          width={"46.5px"}
          style={{ borderRadius: "24px" }}
        />
      </Box>
      <Stack>
        <Typography variant="body2">
          {user.surname}{" "}{user.firstName}
        </Typography>
        <Typography sx={{ color: "text.secondary", mb: 1.5 }}>
          {user.iin}
        </Typography>
      </Stack>
    </Stack>
  );
};

export default UserCardMini;
