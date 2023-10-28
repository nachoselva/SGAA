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

export const PostulacionesTable = (props) => {
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
                  Postulantes
                </TableCell>
                <TableCell>
                  Fecha Postulacion
                </TableCell>
                <TableCell>
                  Fecha Oferta
                </TableCell>
                <TableCell>
                  Estado
                </TableCell>
                <TableCell>
                  
                </TableCell>
                <TableCell>

                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.map((postulacion) => {
                return (
                  <TableRow
                    hover
                    key={postulacion.id}
                  >
                    <TableCell>
                      {postulacion.id}
                    </TableCell>
                    <TableCell>
                      {postulacion.domicilioCompleto}
                    </TableCell>
                    <TableCell>
                      $ {postulacion.montoAlquiler}
                    </TableCell>
                    <TableCell>
                      {postulacion.postulantes}
                    </TableCell>
                    <TableCell>
                      {postulacion.fechaPostulacion}
                    </TableCell>
                    <TableCell>
                      {postulacion.fechaOferta}
                    </TableCell>
                    <TableCell>
                      {postulacion.status}
                    </TableCell>
                    <TableCell>
                      <a href={"aplicacion/" + postulacion.aplicacionId }>Aplicación</a>
                    </TableCell>
                    <TableCell>
                      <a href={"publicacion/" + postulacion.publicacionId}>Publicación</a>
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

PostulacionesTable.propTypes = {
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
