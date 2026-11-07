import { HttpInterceptorFn } from '@angular/common/http';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {
  const url = req.url;
  const endpoint = 'https://localhost:7268/';
  let clone = req.clone({
      url: req.url.replace('/rent/', endpoint)
  });
  return next(clone);
};
