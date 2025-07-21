// src/layouts/MainLayout.tsx
import {
  AppBar,
  Box,
  Button,
  CssBaseline,
  Divider,
  Drawer,
  IconButton,
  Menu,
  MenuItem,
  Toolbar,
  Typography,
  useMediaQuery,
  useTheme,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import Grid from "@mui/material/Grid";
import React from "react";
import { Outlet } from "react-router-dom";
import UserAvatar from "@/features/profile/components/UserAvatar";
import { useTranslation } from "react-i18next";
import { navItems } from "@/app/navigation";
import VerticalMenu from "./VerticalMenu";
import UserCardMini from "@/features/profile/components/UserCardMini";

const MainLayout: React.FC = () => {
  const theme = useTheme();
  const { t, i18n } = useTranslation();

  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);

  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));
  const isTablet = useMediaQuery(theme.breakpoints.between("sm", "md"));

  const [mobileOpen, setMobileOpen] = React.useState(false);

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  const drawerContent = (
    <Box sx={{ p: 1 }}>
      {/* <Typography variant="h6">Меню</Typography> */}
      <Divider sx={{ my: 1 }} />
      <VerticalMenu items={navItems} />
    </Box>
  );

  const drawerWidth = isMobile ? 64 : isTablet ? 180 : 240;

  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const switchLang = () => {
    i18n.changeLanguage(i18n.language === "ru" ? "kk" : "ru");
    handleClose();
  };

  const handleLogout = () => {
    // ClearSession();
    window.location.href = "/login";
  };

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />

      {/* Верхнее меню */}
      <AppBar
        position="fixed"
        sx={{
          zIndex: 1201,
          backgroundColor: theme.palette.background.paper,
          color: theme.palette.text.primary,
          borderBottom: 1,
          borderColor: theme.palette.divider,
          px: { xs: 2, sm: 3 },
          // py: 1,
          maxHeight:"62px"
        }}
      >
        <Toolbar
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          {isMobile && (
            <IconButton
              color="inherit"
              edge="start"
              onClick={handleDrawerToggle}
              sx={{
                mr: 1,
                transition: "transform 0.2s ease",
                ":hover": { transform: "scale(1.1)" },
              }}
            >
              <MenuIcon />
            </IconButton>
          )}
          <Typography
            variant="subtitle1"
            noWrap
            sx={{
              fontWeight: 600,
              letterSpacing: 0.5,
            }}
            component="div"
          >
            {t("app_name")}
          </Typography>
          <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
            <Button
              color="inherit"
              onClick={handleMenu}
              sx={{
                textTransform: "none",
                px: 1,
                minWidth: "unset",
              }}
            >
              <UserAvatar />
            </Button>
            <Menu
              anchorEl={anchorEl}
              open={Boolean(anchorEl)}
              onClose={handleClose}
              slotProps={{
                paper: {
                  elevation: 3,
                  sx: {
                    borderRadius: 2,
                    mt: 1,
                    minWidth: 140,
                    fontSize:"0.8125rem"
                  },
                },
              }}
            >
              <MenuItem sx={{fontSize:"0.8125rem"}}>
                <UserCardMini />
              </MenuItem>
              <MenuItem onClick={switchLang} sx={{fontSize:"0.8125rem"}}>
                {i18n.language?.toUpperCase()}
              </MenuItem>
              <MenuItem onClick={handleLogout} sx={{fontSize:"0.8125rem"}}>
                {t("logout") || "Выход"}
              </MenuItem>
            </Menu>
          </Box>
        </Toolbar>
      </AppBar>

      {/* Боковая панель */}
      {!isMobile && (
        <Drawer
          variant="permanent"
          sx={{
            width: drawerWidth,
            flexShrink: 0,
            [`& .MuiDrawer-paper`]: {
              width: drawerWidth,
              boxSizing: "border-box",
            },
          }}
        >
          <Toolbar />
          {drawerContent}
        </Drawer>
      )}

      {/* Drawer для мобилки (временный) */}
      {isMobile && (
        <Drawer
          variant="temporary"
          open={mobileOpen}
          onClose={handleDrawerToggle}
          ModalProps={{ keepMounted: true }} // ускоряет открытие
          sx={{
            [`& .MuiDrawer-paper`]: {
              width: drawerWidth,
              mt:5
            },
          }}
        >
          {drawerContent}
        </Drawer>
      )}

      {/* Основной контент */}
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          p: 5,
          mt:1,
          ml: 1,//!isMobile ? `${drawerWidth}px` : 0,
          mr:1,
        }}
      >
        <Toolbar />
        {/* Контент страниц */}
        <Grid container spacing={2}>
          <Grid size={12}>
            <Outlet />
          </Grid>
        </Grid>
      </Box>
    </Box>
  );
};

export default MainLayout;
