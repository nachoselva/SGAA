import {
    Box,
    Card, Table,
    TableBody,
    TableCell,
    TableHead,
    TablePagination,
    TableRow
} from '@mui/material';
import PropTypes from 'prop-types';
import { Scrollbar } from '/src/components/scrollbar';

export const PublicacionesTable = (props) => {
  const {
    count = 0,
    items = [],
    onPageChange = () => { },
    onRowsPerPageChange,
    page = 0,
    rowsPerPage = 0,
    selected = []
  } = props;

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
                  Unidad
                </TableCell>
                <TableCell>
                  Monto Alquiler
                </TableCell>
                <TableCell>
                  Disponible Desde
                </TableCell>
                <TableCell>
                  Cantidad Postulaciones
                </TableCell>
                <TableCell>
                  Estado
                </TableCell>
                <TableCell>
                  
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.map((publicacion) => {
                const isSelected = selected.includes(publicacion.id);

                return (
                  <TableRow
                    hover
                    key={publicacion.id}
                  >
                    <TableCell>
                      {publicacion.id}
                    </TableCell>
                    <TableCell>
                      {publicacion.domicilioCompleto}
                    </TableCell>
                    <TableCell>
                      $ {publicacion.montoAlquiler}
                    </TableCell>
                    <TableCell>
                      {publicacion.inicioAlquiler}
                    </TableCell>
                    <TableCell>
                      {publicacion.postulaciones}
                    </TableCell>
                    <TableCell>
                      {publicacion.status}
                    </TableCell>
                    <TableCell>
                      <a href={"publicacion/" + publicacion.id }>Detalle</a>
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
        labelRowsPerPage = "Items por página"
      />
    </Card>
  );
};

PublicacionesTable.propTypes = {
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
