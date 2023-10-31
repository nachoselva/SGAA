import { useMemo } from 'react';
import { applyPagination } from '/src/utils/apply-pagination';

const descendingComparator = (a, b, orderBy) => {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

const getComparator = (order, orderBy) => {
  return order === 'desc'
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

export const useList = (data, lcSearchText, order, orderBy, page, rowsPerPage) => {
  return useMemo(
    () => {
      let resultData = data;
      if (lcSearchText) {
        resultData = resultData.filter((item) =>
          Object.values(item).some(
            field => field?.toString().toLowerCase().includes(lcSearchText)));
      }
      resultData = resultData.sort(getComparator(order, orderBy));
      return { list: applyPagination(resultData, page, rowsPerPage), totalCount: resultData.length };
    },
    [data, lcSearchText, order, orderBy, page, rowsPerPage]
  );
};
