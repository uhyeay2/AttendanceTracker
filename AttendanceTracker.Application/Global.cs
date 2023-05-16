global using AttendanceTracker.Application.Abstraction.BaseHandlers;
global using AttendanceTracker.Application.Abstraction.BaseRequests;
global using AttendanceTracker.Application.Abstraction.Interfaces;

global using AttendanceTracker.Data.Abstraction.Interfaces;

global using AttendanceTracker.Domain.Exceptions;
global using AttendanceTracker.Domain.Extensions;
global using AttendanceTracker.Domain.Interfaces;
global using AttendanceTracker.Domain.Models;
global using AttendanceTracker.Domain.Policy.Validation;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("AttendanceTracker.Application.Tests")]