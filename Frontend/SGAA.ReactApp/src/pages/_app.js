import { CacheProvider } from '@emotion/react';
import { CssBaseline } from '@mui/material';
import { ThemeProvider } from '@mui/material/styles';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import Head from 'next/head';
import { Provider } from "react-redux";
import 'simplebar-react/dist/simplebar.min.css';
import { AuthConsumer, AuthProvider } from '/src/contexts/auth-context';
import { useNProgress } from '/src/hooks/use-nprogress';
import { store } from "/src/stores/store";
import { createTheme } from '/src/theme';
import { createEmotionCache } from '/src/utils/create-emotion-cache';
import { LoadingLayout } from '/src/layouts/loading/layout';

const clientSideEmotionCache = createEmotionCache();

const App = (props) => {
  const { Component, emotionCache = clientSideEmotionCache, pageProps } = props;

  useNProgress();

  const getLayout = Component.getLayout ?? ((page) => page);

  const theme = createTheme();

  return (
    < Provider store={store} >
      <LoadingLayout>
        <CacheProvider value={emotionCache}>
          <Head>
            <title>
              SGAA
            </title>
            <meta
              name="viewport"
              content="initial-scale=1, width=device-width"
            />
          </Head>
          <LocalizationProvider dateAdapter={AdapterDateFns}>
            <AuthProvider>
              <ThemeProvider theme={theme}>
                <CssBaseline />
                <AuthConsumer>
                  {
                    () => getLayout(<Component {...pageProps} />)
                  }
                </AuthConsumer>
              </ThemeProvider>
            </AuthProvider>
          </LocalizationProvider>
        </CacheProvider>
      </LoadingLayout>
    </Provider>
  );
};

export default App;
