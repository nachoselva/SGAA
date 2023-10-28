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

export const UnidadesTable = (props) => {
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
                  Dirección
                </TableCell>
                <TableCell>
                  Localidad
                </TableCell>
                <TableCell>
                  Estado
                </TableCell>
                <TableCell>
                  
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.map((unidad) => {
                const isSelected = selected.includes(unidad.id);

                return (
                  <TableRow
                    hover
                    key={unidad.id}
                    selected={isSelected}
                  >
                    <TableCell>
                      {unidad.id}
                    </TableCell>
                    <TableCell>
                      {unidad.domicilioCompleto}
                    </TableCell>
                    <TableCell>
                      {unidad.ciudad}, {unidad.provincia}
                    </TableCell>
                    <TableCell>
                      {unidad.status}
                    </TableCell>
                    <TableCell>
                      <a href={"unidad/" + unidad.id }>Detalle</a>
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

UnidadesTable.propTypes = {
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
