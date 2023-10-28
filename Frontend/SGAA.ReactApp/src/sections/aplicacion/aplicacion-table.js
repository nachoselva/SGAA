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

export const AplicacionesTable = (props) => {
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
                  Inquilino
                </TableCell>
                <TableCell>
                  Estado
                </TableCell>
                <TableCell>
                  Puntuación
                </TableCell>
                <TableCell>
                  Postulaciones
                </TableCell>
                <TableCell>
                  
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.map((aplicacion) => {
                const isSelected = selected.includes(aplicacion.id);

                return (
                  <TableRow
                    hover
                    key={aplicacion.id}
                    selected={isSelected}
                  >
                    <TableCell>
                      {aplicacion.id}
                    </TableCell>
                    <TableCell>
                      {aplicacion.inquilinoUsuarioNombreCompleto}
                    </TableCell>
                    <TableCell>
                      {aplicacion.status}
                    </TableCell>
                    <TableCell>
                      {aplicacion.puntuacionTotal}
                    </TableCell>
                    <TableCell>
                      {aplicacion.postulaciones}
                    </TableCell>
                    <TableCell>
                      <a href={"aplicacion/" + aplicacion.id }>Detalle</a>
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

AplicacionesTable.propTypes = {
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
