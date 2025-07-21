import {
  Box,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Collapse,
  Typography,
} from "@mui/material";
import { NavLink } from "react-router-dom";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import React, { useState } from "react";

interface MenuItem {
  label: string;
  icon?: React.ReactNode;
  path?: string;
  children?: MenuItem[];
  badge?: React.ReactNode;
}

interface Props {
  items: MenuItem[];
}

const VerticalMenu: React.FC<Props> = ({ items }) => {
  const [openItems, setOpenItems] = useState<Record<string, boolean>>({});

  const handleToggle = (label: string) => {
    setOpenItems((prev) => ({ ...prev, [label]: !prev[label] }));
  };

  return (
    <List component="nav">
      {items.map((item) => (
        <React.Fragment key={item.label}>
          {item.children ? (
            <>
              <ListItemButton onClick={() => handleToggle(item.label)}>
                {item.icon && <ListItemIcon>{item.icon}</ListItemIcon>}
                <ListItemText primary={item.label} />
                {openItems[item.label] ? <ExpandLess /> : <ExpandMore />}
              </ListItemButton>
              <Collapse in={openItems[item.label]} timeout="auto" unmountOnExit>
                <List component="div" disablePadding sx={{ pl: 2 }}>
                  {item.children.map((child) => (
                    <ListItemButton
                      key={child.label}
                      component={NavLink}
                      to={child.path || "#"}
                      sx={{ pl: 3 }}
                    //   className={(isActive) => (isActive ? "active" : "")}
                    >
                      {child.icon && <ListItemIcon>{child.icon}</ListItemIcon>}
                      <ListItemText primary={child.label} />
                      {child.badge}
                    </ListItemButton>
                  ))}
                </List>
              </Collapse>
            </>
          ) : (
            <ListItem disablePadding>
              <ListItemButton component={NavLink} to={item.path || "#"}>
                {item.icon && <ListItemIcon>{item.icon}</ListItemIcon>}
                <ListItemText primary={item.label} />
              </ListItemButton>
            </ListItem>
          )}
        </React.Fragment>
      ))}
    </List>
  );
};

export default VerticalMenu;
