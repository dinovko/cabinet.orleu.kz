import HomeIcon from "@mui/icons-material/Home";
import PersonIcon from "@mui/icons-material/Person";
import CastForEducationIcon from '@mui/icons-material/CastForEducation';
import SettingsIcon from "@mui/icons-material/Settings";
import Groups3Icon from '@mui/icons-material/Groups3';
import QrCodeIcon from '@mui/icons-material/QrCode';

export interface NavItem {
  label: string;
  path: string;
  icon?: React.ReactNode;
  permission?: string; // если нужен контроль доступа
  external?:boolean;
}

export const navItems: NavItem[] = [
  // { label: "Главная", path: "/", icon: <HomeIcon /> },
  { label: "Профиль", path: "/profile", icon: <PersonIcon /> },
  { label: "Курсы", path: "/courses", icon: <CastForEducationIcon /> },
  { label: "Группы", path: "/groups", icon: <Groups3Icon /> },
  { label: "Посещения", path: "https://qr.odx.kz/ru/", icon: <QrCodeIcon />, external:true },
];
