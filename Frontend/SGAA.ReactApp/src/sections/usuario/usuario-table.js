import {
  Box,
  Card, Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
  Link
} from '@mui/material';
import PropTypes from 'prop-types';
import { Scrollbar } from '/src/components/scrollbar';
import { useRouter } from 'next/navigation';

export const UsuariosTable = (props) => {
  const {
    count = 0,
    items = [],
    onPageChange = () => { },
    onRowsPerPageChange,
    page = 0,
    rowsPerPage = 0
  } = props;

  const router = useRouter();

  return (
    <Card>
      <Scrollbar>
        <Box sx={{ minWidth: 800 }}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>
                  #
                </TableCell>
                <TableCell>
                  Nombre
                </TableCell>
                <TableCell>
                  Apellido
                </TableCell>
                <TableCell>
                  Email
                </TableCell>
                <TableCell>
                  Roles
                </TableCell>
                <TableCell>
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.map((usuario) => {
                return (
                  <TableRow
                    hover
                    key={usuario.id}
                  >
                    <TableCell>
                      {usuario.id}
                    </TableCell>
                    <TableCell>
                      {usuario.nombre}
                    </TableCell>
                    <TableCell>
                      {usuario.apellido}
                    </TableCell>
                    <TableCell>
                      {usuario.email}
                    </TableCell>
                    <TableCell>
                      {usuario.roles}
                    </TableCell>
                    <TableCell>
                      <Link
                        component="button"
                        underline="hover"
                        color="inherit"
                        onClick={() => router.push(window.location.pathname + '/' + usuario.id)}>
                        Ver detalle
                      </Link>
                    </TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </Box>
      </Scrollbar>
      <TablePagination
        component="div"
        count={count}
        onPageChange={onPageChange}
        onRowsPerPageChange={onRowsPerPageChange}
        page={page}
        rowsPerPage={rowsPerPage}
        rowsPerPageOptions={[5, 10, 25]}
        labelRowsPerPage="Items por página"
      />
    </Card>
  );
};

UsuariosTable.propTypes = {
  count: PropTypes.number,
  items: PropTypes.array,
  onDeselectAll: PropTypes.func,
  onDeselectOne: PropTypes.func,
  onPageChange: PropTypes.func,
  onRowsPerPageChange: PropTypes.func,
  onSelectAll: PropTypes.func,
  onSelectOne: PropTypes.func,
  page: PropTypes.number,
  rowsPerPage: PropTypes.number,
  selected: PropTypes.array
};
