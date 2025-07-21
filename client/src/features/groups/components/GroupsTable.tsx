import React, { useEffect, useState } from "react";
import {
  Box,
  Collapse,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
  Paper,
  Chip,
  Alert,
  Skeleton,
} from "@mui/material";
import {
  KeyboardArrowDown as KeyboardArrowDownIcon,
  KeyboardArrowUp as KeyboardArrowUpIcon,
} from "@mui/icons-material";
import type { AppDispatch, RootState } from "@/app/store";
import { formatDate, formatDate2 } from "@/utils/dateUtil";
import { useDispatch, useSelector } from "react-redux";
import type { GroupDto } from "../types";
import { fetchCurrentUserGroups } from "../groupsSlice";
// import { fetchCurrentUserGroups } from "../groupsSlice";

const Row: React.FC<{ group: GroupDto }> = ({ group }) => {
  const [open, setOpen] = useState(false);

  return (
    <>
      <TableRow hover>
        <TableCell>
          <IconButton size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell>{group.code}</TableCell>
        <TableCell>
          <Typography fontWeight={500}>{group.course_name}</Typography>
        </TableCell>
        <TableCell>{formatDate2(group.start_date)}</TableCell>
        <TableCell>{formatDate2(group.end_date)}</TableCell>
        <TableCell>{group.sessions_count}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell colSpan={6} sx={{ p: 0, border: "none" }}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box margin={1}>
              <Typography variant="subtitle1" gutterBottom>
                Сессии
              </Typography>
              <Table size="small">
                <TableHead>
                  <TableRow>
                    <TableCell>Дата</TableCell>
                    <TableCell>Сегодня</TableCell>
                    <TableCell>Прибыл</TableCell>
                    <TableCell>Ушел</TableCell>
                    <TableCell>Доверие</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {group.sessions.map((session) => (
                    <TableRow key={session.id}>
                      <TableCell>{formatDate2(session.date)}</TableCell>
                      <TableCell>
                        {session.is_today ? (
                          <Chip label="Сегодня" color="primary" size="small" />
                        ) : (
                          "-"
                        )}
                      </TableCell>
                      <TableCell>
                        {session.attendance ? (
                          <>
                            <div>
                              {new Date(
                                session.attendance.arrived_at
                              ).toLocaleTimeString("ru-RU")}{" "}
                              — {session.attendance.arrived_status_display}
                            </div>
                          </>
                        ) : (
                          "-"
                        )}
                      </TableCell>
                      <TableCell>
                        {session.attendance ? (
                          <>
                            <div>
                              {new Date(
                                session.attendance.left_at
                              ).toLocaleTimeString("ru-RU")}{" "}
                              — {session.attendance.left_status_display}
                            </div>
                          </>
                        ) : (
                          "-"
                        )}
                      </TableCell>
                      <TableCell>
                        {session.attendance ? (
                          <Chip
                            label={session.attendance.trust_level_display}
                            color={
                              session.attendance.trust_level === "trusted"
                                ? "success"
                                : session.attendance.trust_level ===
                                  "suspicious"
                                ? "warning"
                                : "error"
                            }
                            size="small"
                          />
                        ) : (
                          "-"
                        )}
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </>
  );
};

const GroupsTable: React.FC = () => {
  const [open, setOpen] = useState(false);

  const dispatch = useDispatch<AppDispatch>();
  const { data, loading, error } = useSelector(
    (state: RootState) => state.groups
  );

  useEffect(() => {
    dispatch(fetchCurrentUserGroups());
  }, [dispatch]);

  if (loading) return <Skeleton variant="circular" width={40} height={40} />;
  if (error) return <Alert variant="filled" severity="warning"></Alert>;
  if (!data) return null;

  return (
    <TableContainer component={Paper} sx={{ borderRadius: 2 }}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell />
            <TableCell>Код группы</TableCell>
            <TableCell>Название курса</TableCell>
            <TableCell>Начало</TableCell>
            <TableCell>Окончание</TableCell>
            <TableCell>Сессий</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data && data.groups?.map((group) => (
            <Row key={group.id} group={group} />
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default GroupsTable;
