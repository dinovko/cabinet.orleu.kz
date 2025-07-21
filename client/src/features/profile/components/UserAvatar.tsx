// src/components/UserAvatar.tsx
import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { type AppDispatch, type RootState } from "@/app/store";
import { fetchCurrentUser } from "@/features/profile/profileSlice";
import AvatarMale from "@/assets/defaultAvatarMale.png";
import AvatarFemale from "@/assets/defaultAvatarFemale.png";
import { Stack } from "@mui/system";
import { Alert, Skeleton } from "@mui/material";

const UserAvatar: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { user, loading, error } = useSelector(
    (state: RootState) => state.profile
  );

  useEffect(() => {
    dispatch(fetchCurrentUser());
  }, [dispatch]);

  if (loading) return <Skeleton variant="circular" width={40} height={40} />;
  if (error) return <Alert variant="filled" severity="warning"></Alert>;
  if (!user) return null;

  const avatar = user.genderId == "1" ? AvatarMale : AvatarFemale;

  return (
    <Stack>
      <img
        src={avatar}
        alt="Avatar"
        height={"auto"}
        width={"46.5px"}
        style={{ borderRadius: "24px" }}
      />
    </Stack>
    // <div className="flex items-center space-x-2">
    //   <span>{user.surname}{" "}{user.firstName}</span>
    // </div>
  );
};

export default UserAvatar;
