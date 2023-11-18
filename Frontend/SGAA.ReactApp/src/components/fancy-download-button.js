import ArrowDownOnSquareIcon from '@heroicons/react/24/solid/ArrowDownOnSquareIcon';
import { Link, SvgIcon } from '@mui/material';
import React from 'react';

export const FancyDownloadButton = (props) => {
  const { currentFile } = props;

  return (
    <Link
      variant="contained"
      href={currentFile && currentFile.base64}
      download={currentFile && currentFile.name}
      sx={{ 'width': '40px', 'minWidth': '40px', 'height': '40px', 'minHeight': '40px', 'marginTop': '7px', backgroundColor: '#6366F1', display: 'flex', justifyContent: 'center', alignItems: 'center', borderRadius: '12px' }}>
      <SvgIcon sx={{ 'width': '40px', 'minWidth': '40px', 'height': '24px', 'minHeight': '24px', backgroundColor: 'transparent', color: '#ffffff' }}>
        <ArrowDownOnSquareIcon />
      </SvgIcon>
    </Link>
  );

}