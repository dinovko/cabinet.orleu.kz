import type { AppDispatch, RootState } from "@/app/store";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchCurrentUserCourses } from "../courseSlice";
import { Alert, Box, Link, Paper, Skeleton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { formatDate, formatDate2 } from "@/utils/dateUtil";

const CoursesTable = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { user, loading, error } = useSelector(
    (state: RootState) => state.profile
  );

  useEffect(() => {
    // dispatch(fetchCurrentUserCourses());
  }, [dispatch]);

  if (loading) return <Skeleton variant="circular" width={40} height={40} />;
  if (error) return <Alert variant="filled" severity="warning"></Alert>;
  if (!user) return null;

  return (
    <Box>
      <Typography variant="h6" gutterBottom>
        Список курсов
      </Typography>
      <TableContainer component={Paper} sx={{ borderRadius: 2 }}>
        <Table>
          <TableHead>
            <TableRow sx={{ backgroundColor: "#f5f5f5" }}>
              <TableCell>№</TableCell>
              <TableCell>Курс</TableCell>
              <TableCell>Группа</TableCell>
              <TableCell>Дата начала</TableCell>
              <TableCell>Дата окончания</TableCell>
              <TableCell>Сертификат</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {user.courses?.map((course, index) => (
              <TableRow key={course.id} hover>
                <TableCell>{index + 1}</TableCell>
                <TableCell>
                  <Typography fontWeight={500}>{course.courseName}</Typography>
                  <Typography variant="body2" color="text.secondary">
                    ({course.courseCode})
                  </Typography>
                </TableCell>
                <TableCell>{course.groupCode}</TableCell>
                <TableCell>{formatDate2(course.startingDate)}</TableCell>
                <TableCell>
                  {course.endDate ? formatDate2(course.endDate) : "-"}
                </TableCell>
                <TableCell>
                  {course.certificateLink ? (
                    <Link
                      href={course.certificateLink}
                      target="_blank"
                      rel="noopener"
                      underline="hover"
                    >
                      {course.certificateNumber || "Открыть"}
                    </Link>
                  ) : (
                    "-"
                  )}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
};

export default CoursesTable;
