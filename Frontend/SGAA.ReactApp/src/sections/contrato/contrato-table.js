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

export const ContratosTable = (props) => {
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
                  Desde
                </TableCell>
                <TableCell>
                  Hasta
                </TableCell>
                <TableCell>
                  Contrato
                </TableCell>
                <TableCell>
                  Estado
                </TableCell>
                <TableCell>
                  N° Renovación
                </TableCell>
                <TableCell>
                  Inquilinos
                </TableCell>
                <TableCell>
                  Propietarios
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.map((contrato) => {
                return (
                  <TableRow
                    hover
                    key={contrato.id}
                  >
                    <TableCell>
                      {contrato.id}
                    </TableCell>
                    <TableCell>
                      {contrato.domicilio}
                    </TableCell>
                    <TableCell>
                      {contrato.fechaDesde}
                    </TableCell>
                    <TableCell>
                      {contrato.fechaHasta}
                    </TableCell>
                    <TableCell>
                      
                    </TableCell>
                    <TableCell>
                      {contrato.status}
                    </TableCell>
                    <TableCell>
                      {contrato.ordenRenovacion}
                    </TableCell>
                    <TableCell>
                      {contrato.inquilinos}
                    </TableCell>
                    <TableCell>
                      {contrato.propietarios}
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

ContratosTable.propTypes = {
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
