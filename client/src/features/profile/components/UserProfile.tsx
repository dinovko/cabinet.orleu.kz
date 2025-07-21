import React from "react";
import { Box, Grid } from "@mui/system";
import { Avatar, Typography, Card, CardContent } from "@mui/material";
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
  PersonPin,
  LocationCity as LocationCityIcon,
  MenuBook as MenuBookIcon,
  StarRate as StarRateIcon,
  MilitaryTech as MilitaryTechIcon,
} from "@mui/icons-material";
import { useSelector } from "react-redux";
import type { RootState } from "@/app/store";
import { useTranslation } from "react-i18next";
import { formatDate } from "@/utils/dateUtil";

const UserProfile = () => {
  const { t } = useTranslation();
  const { user, loading, error } = useSelector(
    (state: RootState) => state.profile
  );
  return (
    <React.Fragment>
      <Box
        sx={{
          position: "relative",
          height: 200,
          mb: 3,
          borderRadius: 2,
          overflow: "hidden",
          backgroundImage: "linear-gradient(45deg, #3a7bd5 0%, #00d2ff 100%)",
          boxShadow: 3,
        }}
      >
        <Box
          sx={{
            position: "absolute",
            bottom: 10,
            left: "50%",
            transform: "translateX(-50%)",
            zIndex: 2,
          }}
        >
          <Avatar
            src="https://htmlstream.com/preview/front-dashboard-v2.1.1/assets/img/160x160/img1.jpg"
            sx={{
              width: 120,
              height: 120,
              border: "4px solid white",
              boxShadow: 3,
            }}
          />
        </Box>

        {/* Секция ФИО и должности */}
      </Box>
      <Box sx={{ textAlign: "center", mb: 4 }}>
        <Typography variant="h5" fontWeight="bold" gutterBottom>
          {user?.surname} {user?.firstName}
        </Typography>
        <Typography variant="subtitle1" color="text.secondary">
          {user?.isEmployee
            ? user.employeeInform?.empPosition
            : user?.listenerInform?.pedPositionId}
        </Typography>
      </Box>

      {/* Контактная информация - полная ширина */}
      <Card sx={{ mb: 4, boxShadow: 3 }}>
        <CardContent>
          <Typography
            variant="h6"
            fontWeight="bold"
            gutterBottom
            sx={{ mb: 3 }}
          >
            Контактная информация
          </Typography>

          <Grid container spacing={3}>
            <Grid>
              <Box sx={{ display: "flex", alignItems: "center" }}>
                <EmailIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Email
                  </Typography>
                  <Typography>{user?.email}</Typography>
                </Box>
              </Box>
            </Grid>

            <Grid>
              <Box sx={{ display: "flex", alignItems: "center" }}>
                <PhoneIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Телефон
                  </Typography>
                  <Typography>{user?.phone}</Typography>
                </Box>
              </Box>
            </Grid>

            <Grid>
              <Box sx={{ display: "flex", alignItems: "center" }}>
                <LocationIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Регион
                  </Typography>
                  <Typography>{user?.email ?? ""}</Typography>
                </Box>
              </Box>
            </Grid>
          </Grid>
        </CardContent>
      </Card>

      {/* Основная информация - полная ширина */}
      <Card sx={{ mb: 4, boxShadow: 3 }}>
        <CardContent>
          <Typography
            variant="h6"
            fontWeight="bold"
            gutterBottom
            sx={{ mb: 3 }}
          >
            Основная информация
          </Typography>

          <Grid container spacing={3}>
            <Grid>
              <Box sx={{ display: "flex", alignItems: "center" }}>
                <PersonPin color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Национальность
                  </Typography>
                  <Typography>{user?.nationalityId}</Typography>
                </Box>
              </Box>
            </Grid>
            <Grid>
              <Box sx={{ display: "flex", alignItems: "center" }}>
                <PersonIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Пол
                  </Typography>
                  <Typography>
                    {user?.genderId == null
                      ? "➖"
                      : user?.genderId == "1"
                      ? t("female")
                      : t("male")}
                  </Typography>
                </Box>
              </Box>
            </Grid>

            <Grid>
              <Box sx={{ display: "flex", alignItems: "center" }}>
                <CalendarIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Дата рождения
                  </Typography>
                  <Typography>{formatDate(user?.birthDate ?? "")}</Typography>
                </Box>
              </Box>
            </Grid>

            <Grid>
              <Box sx={{ display: "flex", alignItems: "center" }}>
                <WorkIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Педагогический стаж (лет)
                  </Typography>
                  <Typography>{user?.pedExper}</Typography>
                </Box>
              </Box>
            </Grid>

            <Grid>
              <Box sx={{ display: "flex", alignItems: "center" }}>
                {/* <SchoolIcon color="action" sx={{ mr: 2 }} /> */}
                <WorkIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Общий стаж (лет)
                  </Typography>
                  <Typography>{user?.totalEx}</Typography>
                </Box>
              </Box>
            </Grid>
          </Grid>
        </CardContent>
      </Card>
      {/* Информация тренер */}
      {user?.isEmployee == true && (
        <Card sx={{ mb: 4, boxShadow: 3 }}>
          <CardContent>
            <Typography
              variant="h6"
              fontWeight="bold"
              gutterBottom
              sx={{ mb: 3 }}
            >
              Сведения о сотруднике
            </Typography>

            <Grid container spacing={3}>
              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Организация
                    </Typography>
                    <Typography>
                      {user?.employeeInform?.empOrganization ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>
              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Департамент
                    </Typography>
                    <Typography>
                      {user?.employeeInform?.employeeDepartment ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>
              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Должность
                    </Typography>
                    <Typography>
                      {user?.employeeInform?.empPosition ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>
            </Grid>
          </CardContent>
        </Card>
      )}
      {/* Слушатель */}
      {user?.isEmployee == false && (
        <Card sx={{ mb: 4, boxShadow: 3 }}>
          <CardContent>
            <Typography
              variant="h6"
              fontWeight="bold"
              gutterBottom
              sx={{ mb: 3 }}
            >
              Сведения о слушателе
            </Typography>

            <Grid sx={{ width: "100%" }}>
              <Box
                sx={{ display: "flex", alignItems: "center", width: "100%" }}
              >
                <PublicIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Регион
                  </Typography>
                  <Typography>
                    {user?.listenerInform?.regionCode ?? "➖"}
                  </Typography>
                </Box>
              </Box>
            </Grid>

            <Grid sx={{ width: "100%" }}>
              <Box
                sx={{ display: "flex", alignItems: "center", width: "100%" }}
              >
                <LocationCityIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Образовательная организация
                  </Typography>
                  <Typography>
                    {user?.listenerInform?.school ?? "➖"}
                  </Typography>
                  <Typography>
                    {user?.listenerInform?.schoolBIN ?? "➖"}
                  </Typography>
                </Box>
              </Box>
            </Grid>

            <Grid sx={{ width: "100%" }}>
              <Box
                sx={{ display: "flex", alignItems: "center", width: "100%" }}
              >
                <MenuBookIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Предмет
                  </Typography>
                  <Typography>
                    {user?.listenerInform?.pedSubjectId ?? "➖"}
                  </Typography>
                </Box>
              </Box>
            </Grid>

            <Grid sx={{ width: "100%" }}>
              <Box
                sx={{ display: "flex", alignItems: "center", width: "100%" }}
              >
                <WorkIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Должность
                  </Typography>
                  <Typography>
                    {user?.listenerInform?.pedPositionId ?? "➖"}
                  </Typography>
                </Box>
              </Box>
            </Grid>

            <Grid sx={{ width: "100%" }}>
              <Box
                sx={{ display: "flex", alignItems: "center", width: "100%" }}
              >
                <SchoolIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Образование
                  </Typography>
                  <Typography>
                    {user?.listenerInform?.pedEducationTypeId ?? "➖"}
                  </Typography>
                </Box>
              </Box>
            </Grid>

            <Grid sx={{ width: "100%" }}>
              <Box
                sx={{ display: "flex", alignItems: "center", width: "100%" }}
              >
                <StarRateIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Квалификационная категория
                  </Typography>
                  <Typography>
                    {user?.listenerInform?.pedQualCategoryId ?? "➖"}
                  </Typography>
                </Box>
              </Box>
            </Grid>

            <Grid sx={{ width: "100%" }}>
              <Box
                sx={{ display: "flex", alignItems: "center", width: "100%" }}
              >
                <MilitaryTechIcon color="action" sx={{ mr: 2 }} />
                <Box>
                  <Typography variant="subtitle2" color="text.secondary">
                    Учёная степень
                  </Typography>
                  <Typography>
                    {user?.listenerInform?.pedScienceDegreeId ?? "➖"}
                  </Typography>
                </Box>
              </Box>
            </Grid>

            {/* <Grid container spacing={3}>
              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Организация
                    </Typography>
                    <Typography>
                      {user?.listenerInform?.regionCode ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>

              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Организация
                    </Typography>
                    <Typography>
                      {user?.listenerInform?.school ?? "➖"}
                    </Typography>
                    <Typography>
                      {user?.listenerInform?.schoolBIN ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>

              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Организация
                    </Typography>
                    <Typography>
                      {user?.listenerInform?.pedSubjectId ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>

              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Организация
                    </Typography>
                    <Typography>
                      {user?.listenerInform?.pedPositionId ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>

              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Организация
                    </Typography>
                    <Typography>
                      {user?.listenerInform?.pedEducationTypeId ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>

              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Организация
                    </Typography>
                    <Typography>
                      {user?.listenerInform?.pedQualCategoryId ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>

              <Grid sx={{ width: "100%" }}>
                <Box
                  sx={{ display: "flex", alignItems: "center", width: "100%" }}
                >
                  <WorkIcon color="action" sx={{ mr: 2 }} />
                  <Box>
                    <Typography variant="subtitle2" color="text.secondary">
                      Организация
                    </Typography>
                    <Typography>
                      {user?.listenerInform?.pedScienceDegreeId ?? "➖"}
                    </Typography>
                  </Box>
                </Box>
              </Grid>
            </Grid> */}
          </CardContent>
        </Card>
      )}
    </React.Fragment>
  );
};

export default UserProfile;
